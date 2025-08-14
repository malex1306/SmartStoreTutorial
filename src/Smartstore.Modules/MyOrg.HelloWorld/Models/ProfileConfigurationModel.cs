using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Smartstore.Web.Modelling;

namespace MyOrg.HelloWorld.Models;

[Serializable, CustomModelPart]
[LocalizedDisplay("Plugins.MyOrg.HelloWorld.")]
    public class ProfileConfigurationModel
    {
    [LocalizedDisplay("*NumberOfExportedRows")]
    public int NumberOfExportedRows { get; set; }= 10;
    }

