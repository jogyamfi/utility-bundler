using System;
using System.Text;

namespace utility_web_server
{
    public static class JsBundler
    {
        public static string Bundle(string baseLocation)
        {
            StringBuilder sb = new StringBuilder();

            foreach (var file in _bundles)
            {
                if (file.Contains(".min")){
                    continue;
                }

                var fileName = file;

                if (file.IndexOf('/')>-1)
                {
                    fileName = file.Replace('/','\\');
                }

                var fullPath = System.IO.Path.Combine(baseLocation,fileName);

                string fileData = System.IO.File.ReadAllText(fullPath);

                sb.AppendLine(String.Format("//{0} ", fileName));
                sb.AppendLine(" ; " + fileData);
                sb.AppendLine(String.Format("//{0} --end", fileName));
               

            }

            return sb.ToString();
        }


        private static string[] _bundles = new string[] {"js/require.js", "js/prototype.js", "js/localization.js", "js/WebService.js","js/xwiki.js",
                    "rest/config.js","rest/version.js","js/effects.js","js/DateTimePicker.js","js/datepicker.js",
                    "js/searchSuggest.js","js/lock.js","js/livevalidation_prototype.js","js/fullScreen.js", "js/slider.js",
                    "js/dragdrop.js","js/raphael.js", "js/helpers.js","js/queues.js","js/baseGraph.js","js/xcoordclass.js",
                    "js/ordering.js", "js/import.js","js/export.js","js/edgeOptimization.js","js/ageCalc.js","js/positionedGraph.js",
                    "js/dynamicGraph.js","js/Blob.js","js/FileSaver.js","js/nodeMenu.js","js/nodetypeSelectionBubble.js",
                    "js/graphicHelpers.js","js/legend.js","js/disorder.js","js/disorderLegend.js","js/hpoTerm.js",
                    "js/hpoLegend.js","js/geneLegend.js","js/unRenderedLegendSuper.js","js/unRenderedLegend.js",
                    "js/saveLoadIndicator.js","js/templateSelector.js","js/okCancelDialogue.js","js/importSelector.js",
                    "js/exportSelector.js","js/abstractHoverbox.js","js/readonlyHoverbox.js","js/partnershipHoverbox.js",
                    "js/personHoverbox.js","js/abstractNode.js","js/abstractNodeVisuals.js","js/abstractPerson.js",
                    "js/Settings.js","js/undoRedoManager.js","js/controller.js","js/preferencesManager.js",
                    "js/probandDataLoader.js","js/saveLoadEngine.js","js/versionUpdater.js","js/lineSet.js",
                    "js/childlessBehavior.js","js/childlessBehaviorVisuals.js","js/partnershipVisuals.js","js/partnership.js",
                    "js/abstractPersonVisuals.js","js/personVisuals.js","js/person.js","js/personGroupHoverbox.js",
                    "js/personGroupVisuals.js","js/personGroup.js","js/personPlaceholderVisuals.js","js/personPlaceholder.js",
                    "js/view.js","js/svgWrapper.js","js/workspace.js","js/cancersLegend.js","js/printEngine.js",
                    "js/printDialog.js","js/pedigree.js","js/actionButtons.js","js/ContentTopMenu.js","js/PushPatient.js",
                    "js/compatibility.js","js/markerScript.js","js/pedigreeDate.js","js/pedigreeEditorParameters.js",
                    "js/Widgets.js"};


    }
}