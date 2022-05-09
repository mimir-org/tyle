import { NodeLibAm } from "../../models/typeLibrary/application/nodeLibAm";
import { useCreateAspectNode, useGetAspectNodes } from "../../data/queries/typeLibrary/queriesAspectNode";
import {
  ButtonContainer,
  JsonContent,
  QueryAndMutationExampleContainer,
  ResultsContainer,
  StatusAndResultsContainer,
  StatusContainer,
} from "./QueryAndMutationExample.styled";

export const QueryAndMutationExample = () => {
  const mutation = useCreateAspectNode();
  const query = useGetAspectNodes();

  // Fetching a single node by id and caching it with that id and type
  // const nodeQuery = useGetAspectNode("44EEE7F5A7771594F7349A8A230AB272");

  const exampleNode: NodeLibAm = {
    name: "Seperation System",
    rdsName: "Drilling system",
    rdsCode: "A",
    purposeName: "Heat (Electrical)",
    aspect: 2,
    description: "Liten beskrivelse",
    symbol: "01A6DC68286629E8D4C415C14BD75D8D",
    nodeTerminals: [
      {
        terminalId: "27634FF8002B12E75D98E07CCD005D18",
        number: 2,
        connectorDirection: 0,
      },
      {
        terminalId: "27634FF8002B12E75D98E07CCD005D18",
        number: 2,
        connectorDirection: 1,
      },
    ],
    attributeIdList: ["07AFF47B9870A2D1B697F6F319A7185C", "11845C1C348E28563B4EA4B2E960B04C"],
    selectedAttributePredefined: [],
    simpleIdList: [],
    attributeAspectIri: "",
    companyId: 101,
    parentId: "",
    version: "1.0",
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
