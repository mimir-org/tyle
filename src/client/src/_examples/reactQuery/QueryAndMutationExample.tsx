import { State } from "../../models/typeLibrary/enums/state";
import { Aspect } from "../../models/typeLibrary/enums/aspect";
import { ConnectorDirection } from "../../models/typeLibrary/enums/connectorDirection";
import { NodeLibAm } from "../../models/typeLibrary/application/nodeLibAm";
import { useCreateAspectNode, useGetAspectNodes } from "../../data/queries/typeLibrary/queriesAspectNode";
import {
  ResultsContainer,
  ButtonContainer,
  QueryAndMutationExampleContainer,
  StatusAndResultsContainer,
  JsonContent,
  StatusContainer,
} from "./QueryAndMutationExample.styled";

export const QueryAndMutationExample = () => {
  const mutation = useCreateAspectNode();
  const query = useGetAspectNodes();

  // Fetching a single node by id and caching it with that id and type
  // const nodeQuery = useGetAspectNode("44EEE7F5A7771594F7349A8A230AB272");

  const exampleNode: NodeLibAm = {
    name: `Separation System ${query.data?.length}`,
    rdsId: "01501C14F3073D6C1140B7BB1BAC56D8",
    purposeId: "24B7E9F78BB9A5278129B46DC27C94C2",
    parentId: null,
    version: null,
    firstVersionId: null,
    aspect: Aspect.Function,
    state: State.Draft,
    companyId: 101,
    description: "Description for this node",
    blobId: "018120B75674ABF18AB2F07691D1865C",
    attributeAspectId: null,
    nodeTerminals: [
      {
        terminalId: "27634FF8002B12E75D98E07CCD005D18",
        number: 1,
        connectorDirection: ConnectorDirection.Input,
      },
      {
        terminalId: "27634FF8002B12E75D98E07CCD005D18",
        number: 1,
        connectorDirection: ConnectorDirection.Output,
      },
    ],
    attributeIdList: ["07AFF47B9870A2D1B697F6F319A7185C", "11845C1C348E28563B4EA4B2E960B04C"],
    selectedAttributePredefined: null,
    simpleIdList: null,
    collectionIdList: null,
    updatedBy: null,
    updated: null,
    created: new Date().toISOString(),
    createdBy: "Example User",
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
        <button onClick={() => mutation.mutate({ ...exampleNode, rdsId: "INVALID_ID" })}>Add invalid node ❌</button>
      </ButtonContainer>
    </QueryAndMutationExampleContainer>
  );
};
