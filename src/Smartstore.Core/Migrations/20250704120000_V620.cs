﻿using FluentMigrator;
using Smartstore.Core.Common.Configuration;
using Smartstore.Core.Configuration;
using Smartstore.Core.Security;
using Smartstore.Data.Migrations;

namespace Smartstore.Core.Data.Migrations
{
    [MigrationVersion("2025-07-04 12:00:00", "V620")]
    internal class V620 : Migration, ILocaleResourcesProvider, IDataSeeder<SmartDbContext>
    {
        public override void Up()
        {
        }

        public override void Down()
        {
        }

        public DataSeederStage Stage => DataSeederStage.Early;
        public bool AbortOnFailure => false;

        public async Task SeedAsync(SmartDbContext context, CancellationToken cancelToken = default)
        {
            await context.MigrateLocaleResourcesAsync(MigrateLocaleResources);
        }

        public void MigrateLocaleResources(LocaleResourcesBuilder builder)
        {
            builder.AddOrUpdate("Aria.Label.MainNavigation", "Main navigation", "Hauptnavigation");
            builder.AddOrUpdate("Aria.Label.PageNavigation", "Page navigation", "Seitennavigation");
            builder.AddOrUpdate("Aria.Label.OffCanvasMenuTab", "Shop sections", "Shopbereiche");
            builder.AddOrUpdate("Aria.Label.ShowPreviousProducts", "Show previous product group", "Vorherige Produktgruppe anzeigen");
            builder.AddOrUpdate("Aria.Label.ShowNextProducts", "Show next product group", "Nächste Produktgruppe anzeigen");
            builder.AddOrUpdate("Aria.Label.CommentForm", "Comment form", "Kommentarformular");
            builder.AddOrUpdate("Aria.Label.Breadcrumb", "Breadcrumb", "Breadcrumb-Navigation");
            builder.AddOrUpdate("Aria.Label.MediaGallery", "Media gallery", "Mediengalerie");

            builder.AddOrUpdate("Aria.Label.SearchFilters", "Search filters", "Suchfilter");
            builder.AddOrUpdate("Aria.Label.SelectDeselectEntries", "Select or deselect all entries in the list", "Alle Einträge der Liste aus- oder abwählen");
            builder.AddOrUpdate("Aria.Label.SelectDeselectEntry", "Select or deselect entry", "Eintrag aus- oder abwählen");

            builder.AddOrUpdate("Aria.Description.SearchBox",
                "Enter a search term. Press the Enter key to view all the results.",
                "Geben Sie einen Suchbegriff ein. Drücken Sie die Eingabetaste, um alle Ergebnisse aufzurufen.");
            builder.AddOrUpdate("Aria.Description.InstantSearch",
                "Enter a search term. Results will appear automatically as you type. Press the Enter key to view all the results.",
                "Geben Sie einen Suchbegriff ein. Während Sie tippen, erscheinen automatisch erste Ergebnisse. Drücken Sie die Eingabetaste, um alle Ergebnisse aufzurufen.");
            builder.AddOrUpdate("Aria.Description.AutoSearchBox",
                "Enter a search term. Results will appear automatically as you type.",
                "Geben Sie einen Suchbegriff ein. Die Ergebnisse erscheinen automatisch, während Sie tippen.");

            builder.AddOrUpdate("Aria.Label.CurrencySelector", "Current currency {0} - Change currency", "Aktuelle Währung {0} – Währung wechseln");
            builder.AddOrUpdate("Aria.Label.LanguageSelector", "Current language {0} - Change language", "Aktuelle Sprache {0} – Sprache wechseln");
            builder.AddOrUpdate("Aria.Label.SocialMediaLinks", "Our social media channels", "Unsere Social-Media-Kanäle");
            builder.AddOrUpdate("Aria.Label.Rating", "Rating: {0} out of 5 stars. {1}", "Bewertung: {0} von 5 Sternen. {1}");
            builder.AddOrUpdate("Aria.Label.ExpandItem", "Press ENTER for more options to {0}", "Drücken Sie ENTER für mehr Optionen zu {0}");
            builder.AddOrUpdate("Aria.Label.ProductOfOrderPlacedOn", "Order {0} from {1}, {2}", "Auftrag {0} vom {1}, {2}");
            builder.AddOrUpdate("Aria.Label.PaginatorItemsPerPage", "Results per page:", "Ergebnisse pro Seite:");
            builder.AddOrUpdate("Aria.Label.ApplyPriceRange", "Apply price range", "Preisbereich anwenden");
            builder.AddOrUpdate("Aria.Label.PriceRange", "Price range", "Preisspanne");
            builder.AddOrUpdate("Aria.Label.UploaderProgressBar", "{0} fileupload", "{0} Dateiupload");
            builder.AddOrUpdate("Aria.Label.ShowPassword", "Show password", "Passwort anzeigen");
            builder.AddOrUpdate("Aria.Label.HidePassword", "Hide password", "Passwort verbergen");
            builder.AddOrUpdate("Aria.Label.CheckoutProcess", "Checkout process", "Bestellprozess");
            builder.AddOrUpdate("Aria.Label.CheckoutStep.Visited", "Completed", "Abgeschlossen");
            builder.AddOrUpdate("Aria.Label.CheckoutStep.Current", "Current step", "Aktueller Schritt");
            builder.AddOrUpdate("Aria.Label.CheckoutStep.Pending", "Not visited", "Noch nicht besucht");
            builder.AddOrUpdate("Aria.Label.SearchHitCount", "Search results", "Suchergebnisse");
            // INFO: Must be generic for Cart, Compare & Wishlist
            builder.AddOrUpdate("Aria.Label.OffCanvasCartTab", "My articles", "Meine Artikel");

            builder.AddOrUpdate("Search.SearchBox.Clear", "Clear search term", "Suchbegriff löschen");
            builder.AddOrUpdate("Common.ScrollUp", "Scroll up", "Nach oben scrollen");
            builder.AddOrUpdate("Common.SelectAction", "Select action", "Aktion wählen");
            builder.AddOrUpdate("Common.ExpandCollapse", "Expand/collapse", "Auf-/zuklappen");
            builder.AddOrUpdate("Common.DeleteSelected", "Delete selected", "Ausgewählte löschen");
            builder.AddOrUpdate("Common.Consent", "Consent", "Zustimmung");
            builder.AddOrUpdate("Common.SelectView", "Select view", "Ansicht wählen");
            builder.AddOrUpdate("Common.SecurityPrompt", "Security prompt", "Sicherheitsabfrage");
            builder.AddOrUpdate("Common.SkipToMainContent", "Skip to main content", "Zum Hauptinhalt springen");

            builder.Delete(
                "Account.BackInStockSubscriptions.DeleteSelected",
                "PrivateMessages.Inbox.DeleteSelected");

            builder.AddOrUpdate("Admin.Configuration.Settings.General.Common.Captcha.Hint",
                "CAPTCHAs are used for security purposes to help distinguish between human and machine users. They are typically used to verify that internet forms are being"
                + " filled out by humans and not robots (bots), which are often misused for this purpose. reCAPTCHA accounts are created at <a"
                + " class=\"alert-link\" href=\"https://cloud.google.com/security/products/recaptcha?hl=en\" target=\"_blank\">Google</a>. Select <b>Task (v2)</b> as the reCAPTCHA type.",
                "CAPTCHAs dienen der Sicherheit, indem sie dabei helfen, zu unterscheiden, ob ein Nutzer ein Mensch oder eine Maschine ist. In der Regel wird diese Funktion genutzt,"
                + " um zu überprüfen, ob Internetformulare von Menschen oder Robotern (Bots) ausgefüllt werden, da Bots hier oft missbräuchlich eingesetzt werden."
                + " reCAPTCHA-Konten werden bei <a class=\"alert-link\" href=\"https://cloud.google.com/security/products/recaptcha?hl=de\" target=\"_blank\">Google</a>"
                + " angelegt. Wählen Sie als reCAPTCHA-Typ <b>Aufgabe (v2)</b> aus.");

            builder.AddOrUpdate("Polls.TotalVotes", "{0} votes cast.", "{0} abgegebene Stimmen.");

            builder.AddOrUpdate("Blog.RSS.Hint",
                "Opens the RSS feed with the latest blog posts. Subscribe with an RSS reader to stay informed.",
                "Öffnet den RSS-Feed mit aktuellen Blogbeiträgen. Mit einem RSS-Reader abonnieren und informiert bleiben.");

            builder.AddOrUpdate("News.RSS.Hint",
                "Opens the RSS feed with the latest news. Subscribe with an RSS reader to stay informed.",
                "Öffnet den RSS-Feed mit aktuellen News. Mit einem RSS-Reader abonnieren und informiert bleiben.");

            builder.AddOrUpdate("Order.CannotCompleteUnpaidOrder",
                "An order with a payment status of \"{0}\" cannot be completed.",
                "Ein Auftrag mit dem Zahlungsstatus \"{0}\" kann nicht abgeschlossen werden.");

            builder.AddOrUpdate("Account.CustomerOrders.RecurringOrders.Cancel",
                "Cancel repeat delivery for order {0}",
                "Regelmäßige Lieferung für Auftrag {0} abbrechen");

            builder.AddOrUpdate("Account.Avatar.AvatarChanged", "The avatar has been changed.", "Der Avatar wurde geändert.");
            builder.AddOrUpdate("Account.Avatar.AvatarRemoved", "The avatar has been removed.", "Der Avatar wurde entfernt.");

            builder.AddOrUpdate("RewardPoints.History", "History of your reward points", "Ihr Bonuspunkteverlauf");

            builder.AddOrUpdate("Reviews.Overview.NoReviews",
                "There are no reviews for this product yet.",
                "Zu diesem Produkt liegen noch keine Bewertungen vor.");

            builder.AddOrUpdate("DownloadableProducts.IAgree",
                "I have read and agree to the user agreement.",
                "Ich habe die Nutzungsvereinbarung gelesen und bin einverstanden.");

            builder.AddOrUpdate("Common.FormFields.Required.Hint",
                "* Input fields with an asterisk are mandatory and must be filled in.",
                "* Eingabefelder mit Sternchen sind Pflichtfelder und müssen ausgefüllt werden.");


            builder.Delete("Categories.Breadcrumb.Top");

            builder.AddOrUpdate("Order.ShipmentStatusEvents", "Status of your shipment", "Status Ihrer Sendung");
            builder.AddOrUpdate("BackInStockSubscriptions.PopupTitle", "Email when available", "E-Mail bei Verfügbarkeit");
        }
    }
}
