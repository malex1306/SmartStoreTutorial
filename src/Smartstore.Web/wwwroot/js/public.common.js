﻿(function ($, window, document, undefined) {

    var viewport = ResponsiveBootstrapToolkit;

    window.displayAjaxLoading = function (display) {
        if ($.throbber === undefined)
            return;

        if (display) {
            $.throbber.show({ speed: 50, white: true });
        }
        else {
            $.throbber.hide();
        }
    };

    window.getPageWidth = function () {
        return parseFloat($("#page").css("width"));
    };

    window.getViewport = function () {
        return viewport;
    };

    var _commonPluginFactories = [
        // select2
        function (ctx) {
            if ($.fn.select2 === undefined || $.fn.selectWrapper === undefined)
                return;
            ctx.find("select:not(.noskin), input:hidden[data-select]").selectWrapper();
        },
        // Tooltips
        function (ctx) {
            if ($.fn.tooltip === undefined)
                return;
            var selector = window.touchable
                ? '[data-toggle=tooltip].tooltip-toggle-touch, .tooltip-toggle.tooltip-toggle-touch'
                : '[data-toggle=tooltip], .tooltip-toggle';
            ctx.tooltip({
                selector: selector,
                animation: true,
                trigger: 'hover focus'
            });
        },
        // Popovers
        function (ctx) {
            if ($.fn.popover === undefined)
                return;
            ctx.find(".popover-trigger").popover();
        },
        // NumberInput
        function (ctx) {
            if ($.fn.numberInput === undefined)
                return;
            ctx.find(".numberinput-group").numberInput();
        },
        // QuantityInput
        function (ctx) {
            ctx.find('.qty-input .numberinput').on("change", function () {
                var el = $(this);
                var qtyUnitName = el.val() > 1 && el.data("qtyunit-plural") ? el.data("qtyunit-plural") : el.data("qtyunit-singular");
                el.closest(".qty-input").find(".numberinput-postfix").text(qtyUnitName);
            });
        },
        // Copy to clipboard button
        function (ctx) {
            connectCopyToClipboard(ctx.find(".btn-clipboard"));
        },
        // Newsletter subsription
        function (ctx) {
            let newsletterForm = $('#newsletter-form');
            if (newsletterForm.length > 0) {
                newsletterForm.find('#newsletter-subscribe-button').on("click", function (e) {
                    e.preventDefault();

                    var email = $("#newsletter-email").val();
                    var subscribe = 'true';
                    var resultDisplay = $("#newsletter-result-block");
                    var elemGdprConsent = $(".footer-newsletter .gdpr-consent-check");
                    var gdprConsent = elemGdprConsent.length == 0 ? null : elemGdprConsent.is(':checked');

                    if ($('#newsletter-unsubscribe').is(':checked')) {
                        subscribe = 'false';
                    }

                    $.ajax({
                        cache: false,
                        type: "POST",
                        url: newsletterForm.attr('action'),
                        data: { "subscribe": subscribe, "email": email, "GdprConsent": subscribe == 'true' ? gdprConsent : true },
                        success: function (data) {
                            resultDisplay.html(data.Result);
                            if (data.Success) {
                                $('#newsletter-subscribe-block').hide();
                                resultDisplay.removeClass("alert-danger d-none").addClass("alert-success d-block");
                            }
                            else {
                                if (data.Result != "") {
                                    resultDisplay.removeClass("alert-success d-none").addClass("alert-danger d-block").fadeIn("slow").delay(2000).fadeOut("slow");
                                }
                            }
                        },
                        error: function (xhr, ajaxOptions, thrownError) {
                            resultDisplay.empty()
                                .text(newsletterForm.data('subscription-failure'))
                                .removeClass("alert-success d-none")
                                .addClass("alert-danger d-block");
                        }
                    });
                    return false;
                });
            }
        },
        // Slick carousel
        function (ctx) {
            if ($.fn.slick === undefined)
                return;

            ctx.find('.artlist-carousel > .artlist-grid').each(function (i, el) {
                const list = $(this);
                const slidesToShow = list.data("slides-to-show");
                const slidesToScroll = list.data("slides-to-scroll");
                const labelPrev = list.data("label-prev") || '';
                const labelNext = list.data("label-next") || '';

                if (list.hasClass('slick-initialized')) {
                    list.slick('destroy');
                    list.off('.slick');
                }

                list.on('init.slick', (_, slick) => {
                    const $track = slick.$slideTrack;

                    // INFO: Roles such as "list" and "listItem" are problematic. Conflicts with aria-hidden=true of slides for instance.
                    // Better/simpler: Fallback to a minimum context by using role=group plus aria-label on a parent element.
                    slick.$slider.removeAttr('role');
                    $track.find('.slick-slide').removeAttr('role');

                    // Remove any .sr-toggle in .art
                    $track.find('> .art > .sr-toggle').remove();
                });

                list.slick({
                    rtl: $("html").attr("dir") == "rtl",
                    dots: true,
                    cssEase: 'ease-in-out',
                    speed: 300,
                    useCSS: true,
                    useTransform: true,
                    waitForAnimate: true,
                    prevArrow: `<button type="button" class="btn btn-secondary slick-prev" aria-label="${labelPrev}"><i class="fa fa-chevron-left" aria-hidden="true"></i></button>`,
                    nextArrow: `<button type="button" class="btn btn-secondary slick-next" aria-label="${labelNext}"><i class="fa fa-chevron-right" aria-hidden="true"></i></button>`,
                    respondTo: 'slider',
                    slidesToShow: slidesToShow || 6,
                    slidesToScroll: slidesToScroll || 6,
                    autoplay: list.data("autoplay"),
                    infinite: list.data("infinite"),
                    accessibility: true,
                    atomicTabbing: false,
                    responsive: [
                        {
                            breakpoint: 280,
                            settings: {
                                slidesToShow: Math.min(slidesToShow || 1, 1),
                                slidesToScroll: Math.min(slidesToScroll || 1, 1)
                            }
                        },
                        {
                            breakpoint: 440,
                            settings: {
                                slidesToShow: Math.min(slidesToShow || 2, 2),
                                slidesToScroll: Math.min(slidesToScroll || 2, 2)
                            }
                        },
                        {
                            breakpoint: 640,
                            settings: {
                                slidesToShow: Math.min(slidesToShow || 3, 3),
                                slidesToScroll: Math.min(slidesToScroll || 3, 3)
                            }
                        },
                        {
                            breakpoint: 780,
                            settings: {
                                slidesToShow: Math.min(slidesToShow || 4, 4),
                                slidesToScroll: Math.min(slidesToScroll || 4, 4)
                            }
                        },
                        {
                            breakpoint: 960,
                            settings: {
                                slidesToShow: Math.min(slidesToShow || 5, 5),
                                slidesToScroll: Math.min(slidesToScroll || 5, 5)
                            }
                        },
                    ]
                });
            });
        }
    ];

    /* 
        Helpful in AJAX scenarios, where jQuery plugins has to be applied 
        to newly created html.
    */

    window.applyCommonPlugins = function (/* jQuery */ context) {
        $.each(_commonPluginFactories, function (i, val) {
            val.call(this, $(context));
        });
    };

    // on document ready
    // TODO: reorganize > public.globalinit.js

    let hasAOS = typeof AOS !== 'undefined';

    $(function () {
        // Init reveal on scroll with AOS library
        if (hasAOS
            && !$('body').hasClass('no-reveal')
            && !$('body').attr("data-aos-duration")) {
            AOS.init({ once: true, duration: 1000 });
        }

        if (Smartstore.parallax !== undefined && !$('body').hasClass('no-parallax')) {
            Smartstore.parallax.init({
                context: document.body,
                selector: '.parallax'
            });
        }

        applyCommonPlugins($("body"));
    });

    if (hasAOS) {
        window.addEventListener('load', AOS.refresh);
    }

})(jQuery, this, document);

