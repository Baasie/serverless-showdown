using Microsoft.Azure.Cosmos.Table;
using System;

namespace ServerlessParking.Repositories.Builders
{
    public static class RepositoryExceptionBuilder
    {
        public static RepositoryException CreateExceptionForCollectionCreation(Uri databaseUri, string collectionId, Exception exception)
        {
            return new RepositoryException($"{ErrorPrefix} creating collection {collectionId} in database {databaseUri.AbsoluteUri}.", exception);
        }

        public static RepositoryException CreateExceptionForDocumentRead(Uri documentUri, Exception exception)
        {
            return new RepositoryException($"{ErrorPrefix} retrieving document {documentUri.AbsoluteUri}.", exception);
        }

        public static RepositoryException CreateExceptionForDocumentQuery(string propertyName, string propertyValue, Exception exception)
        {
            return new RepositoryException($"{ErrorPrefix} retrieving document with {propertyName}={propertyValue}.", exception);
        }

        public static RepositoryException CreateExceptionForDocumentCreation<T>(T objectToCreate, Exception exception)
        {
            return new RepositoryException($"{ErrorPrefix} creating a new {nameof(T)}.", exception);
        }

        public static RepositoryException CreateExceptionForDocumentDelete(Uri documentUri, Exception exception)
        {
            return new RepositoryException($"{ErrorPrefix} deleting document {documentUri.AbsoluteUri}.", exception);
        }

        public static RepositoryException CreateExceptionForDocumentUpdate(Uri documentUri, Exception exception)
        {
            return new RepositoryException($"{ErrorPrefix} updating document {documentUri.AbsoluteUri}.", exception);
        }

        public static RepositoryException CreateExceptionForTableCreation(CloudTable table, Exception exception)
        {
            return new RepositoryException($"{ErrorPrefix} creating table {table.Name}.", exception);
        }

        public static RepositoryException CreateExceptionForTableOperation(ITableEntity entity, CloudTable table, TableOperation tableOperation, Exception exception)
        {
            return new RepositoryException($"{ErrorPrefix} performing a {tableOperation.OperationType.ToString()} operation on table {table.Name} with partitionKey {entity.PartitionKey} and rowKey {entity.RowKey}.", exception);
        }

        public static RepositoryException CreateExceptionForTableOperation(string partitionKey, string rowKey, CloudTable table, TableOperation tableOperation, Exception exception)
        {
            return new RepositoryException($"{ErrorPrefix} performing a {tableOperation.OperationType.ToString()} operation on table {table.Name} with partitionKey {partitionKey} and rowKey {rowKey}.", exception);
        }

        public static RepositoryException CreateExceptionForTableQuery<T>(CloudTable table, TableQuery<T> query, Exception exception)
        {
            return new RepositoryException($"{ErrorPrefix} querying table {table.Name} with {query.FilterString}.", exception);
        }

        private const string ErrorPrefix = "An error occurred while";
    }
}
