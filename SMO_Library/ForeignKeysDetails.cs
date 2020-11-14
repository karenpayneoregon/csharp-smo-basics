namespace SMO_Library 
{
    public class ForeignKeysDetails
    {
        /// <summary>
        /// Schema of table
        /// </summary>
        public string TableSchema { get; set; }
        /// <summary>
        /// Table name
        /// </summary>
        public string TableName { get; set; }
        /// <summary>
        /// Schema name
        /// </summary>
        public string SchemaName { get; set; }
        public override string ToString()
        {
            return SchemaName;
        }
    }
}
