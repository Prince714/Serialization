// https://docs.microsoft.com/en-us/nuget/consume-packages/install-use-packages-visual-studio

using System.IO;
using System.Linq;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

using Psim.Materials;

namespace Psim.IOManagers
{
    public static class InputManager
    {
        public static Model InitializeModel(string path)
        {
            JObject modelData = LoadJson(path);
            // This model can only handle 1 material
            Material material = GetMaterial(modelData["materials"][0]);
            Model model = GetModel(material, modelData["settings"]);
            AddSensors(model, modelData["sensors"]);
            AddCells(model, modelData["cells"]);
            return model;
        }
        private static JObject LoadJson(string path)
        {
            using (StreamReader r = new StreamReader(path))
            {
                string json = r.ReadToEnd();
                JObject modelData = JObject.Parse(json);
                return modelData;
            }
        }

        private static void AddCells(Model m, JToken cellData)
        {
            throw new System.NotImplementedException();
        }

        private static void AddSensors(Model m, JToken sensorData)
        {
            throw new System.NotImplementedException();
        }

        private static Model GetModel(Material material, JToken settingsData)
        {
            throw new System.NotImplementedException();
        }

        private static Material GetMaterial(JToken materialData)
        {
            var dData = GetDispersionData(materialData["d_data"]);
            var rData = GetRelaxationData(materialData["r_data"]);
            return new Material(dData, rData);
        }

        private static DispersionData GetDispersionData(JToken dData)
        {
            var wMaxLa = (double)dData["max_freq_la"];
            var wMaxTa = (double)dData["max_freq_ta"];
            var laData = dData["la_data"].ToObject<double[]>();
            var taData = dData["ta_data"].ToObject<double[]>();
            return new DispersionData(laData, wMaxLa, taData, wMaxTa);
        }

        private static RelaxationData GetRelaxationData(JToken rData)
        {
            var bl = (double)rData["b_l"];
            var btn = (double)rData["b_tn"];
            var btu = (double)rData["b_tu"];
            var bi = (double)rData["b_bi"];
            var w = (double)rData["w"];
            return new RelaxationData(bl, btn, btu, bi, w);


        }
    }
}
