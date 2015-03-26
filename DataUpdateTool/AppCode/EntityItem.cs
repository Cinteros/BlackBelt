﻿using Cinteros.Xrm.XmlEditorUtils;
using Microsoft.Xrm.Sdk.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinteros.Xrm.DataUpdateTool.AppCode
{
    public class EntityItem : IComboBoxItem
    {
        private EntityMetadata meta = null;

        public EntityItem(EntityMetadata Entity)
        {
            meta = Entity;
        }

        public override string ToString()
        {
            return DataUpdater.GetEntityDisplayName(meta);
        }

        public string GetValue()
        {
            return meta.LogicalName;
        }
    }
}
