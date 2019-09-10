namespace wf_pickInformation
{
     public class ElementData
    {
        //PROPIEDADES
        private string _id;
        private string _elemento;
        private string _nivel;
        private string _volumen;

        //CONSTRUCTOR 
        public ElementData(
            string id,
            string elemento,
            string nivel,
            string  volumen)
        {
            _id = id;
            _elemento = elemento;
            _nivel = nivel;
            _volumen = volumen;
        }
        //METODOS
        public string Id
        {
            get { return _id; }
        }
        public string Elemento
        {
            get { return _elemento; }
        }
        public string Nivel
        {
            get { return _nivel; }
        }
        public string Volumen
        {
            get { return _volumen; }
        }
    }
}
