namespace GraphicsControls
{
    public interface IGraphicsLayerManager
    {
        int GetLayerIndex(string layerName);

        void AddLayer(string layer);

        void AddLayer(int index, string layer);

        void RemoveLayer(string layer);

        void RemoveLayer(int index);
    }
}