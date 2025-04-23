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
    uri: 'http://localhost:8401/graphql/', // Replace with your actual GraphQL server URL
  }),
  cache: new InMemoryCache(),
});

export default apolloClient;
