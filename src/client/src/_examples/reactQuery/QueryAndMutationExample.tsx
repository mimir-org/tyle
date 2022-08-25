import { Aspect, NodeLibAm } from "@mimirorg/typelibrary-types";
import { useCreateNode, useGetNodes } from "../../data/queries/tyle/queriesNode";
import {
  ButtonContainer,
  JsonContent,
  QueryAndMutationExampleContainer,
  ResultsContainer,
  StatusAndResultsContainer,
  StatusContainer,
} from "./QueryAndMutationExample.styled";

export const QueryAndMutationExample = () => {
  const mutation = useCreateNode();
  const query = useGetNodes();

  // Fetching a single node by id and caching it with that id and type
  // const nodeQuery = useGetAspectNode("44EEE7F5A7771594F7349A8A230AB272");

  const exampleNode: NodeLibAm = {
    name: "Test node",
    rdsCode: "A",
    rdsName: "Drilling system",
    purposeName: "Heat (Electrical)",
    parentId: "",
    aspect: Aspect.Function,
    companyId: 0,
    description: "A description goes here",
    symbol: "http://localhost:5001/symbol/018120B75674ABF18AB2F07691D1865C.svg",
    attributeIdList: ["07AFF47B9870A2D1B697F6F319A7185C", "11845C1C348E28563B4EA4B2E960B04C"],
    nodeTerminals: [
      {
        terminalId: "BFB27765A609F27FD3A2DF59C4E4D63D",
        quantity: 1,
        connectorDirection: 0,
      },
      {
        terminalId: "54CC2AB379D04FA1B6A6B83A962B0CF2",
        quantity: 1,
        connectorDirection: 1,
      },
    ],
    selectedAttributePredefined: [],
    simpleIdList: [],
    typeReferences: [],
  };

  return (
    <QueryAndMutationExampleContainer>
      <h1>Nodes example</h1>
      <p>Throttle the network speed in your browser to view state changes more clearly</p>

      <StatusAndResultsContainer>
        <StatusContainer>
          <div>
            <b>Query status: </b>
            <i>
              {query.isLoading && "Loading nodes 🔄"}
              {query.isFetching && !query.isLoading && "Fetching nodes after changes 🔄"}
              {query.isSuccess && !query.isLoading && !query.isFetching && "Success fetching nodes ✅"}
              {query.isError && "Error fetching nodes ❌"}
            </i>
          </div>

          <div>
            <b>Mutation status: </b>
            <i>
              {mutation.isLoading && "Creating node 🔄"}
              {mutation.isSuccess && "Success creating node ✅"}
              {mutation.isError && "Error creating node ❌"}
            </i>
            {mutation.isError && <JsonContent>{JSON.stringify(mutation.error, null, 2)}</JsonContent>}
          </div>
        </StatusContainer>

        <ResultsContainer>
          {query.data?.map((n) => (
            <JsonContent key={n.id}>{JSON.stringify(n, null, 2)}</JsonContent>
          ))}
        </ResultsContainer>
      </StatusAndResultsContainer>

      <ButtonContainer>
        <button onClick={() => mutation.mutate(exampleNode)}>Add valid node ✅</button>
        <button onClick={() => mutation.mutate({ ...exampleNode, parentId: "INVALID_ID" })}>Add invalid node ❌</button>
      </ButtonContainer>
    </QueryAndMutationExampleContainer>
  );
};
