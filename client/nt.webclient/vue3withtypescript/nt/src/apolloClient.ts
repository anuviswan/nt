// apolloClient.ts
import {
  ApolloClient,
  InMemoryCache,
  HttpLink,
  NormalizedCacheObject,
} from '@apollo/client/core';

// Create a new Apollo Client instance
const apolloClient: ApolloClient<NormalizedCacheObject> = new ApolloClient({
  link: new HttpLink({
    uri: import.meta.env.VITE_APP_GRAPHQL_URL,//'http://localhost:8001/movies', // Replace with your actual GraphQL server URL
  }),
  cache: new InMemoryCache(),
});

export default apolloClient;
