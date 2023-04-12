import { Aspect, AspectObjectLibAm } from "@mimirorg/typelibrary-types";
import { useCreateAspectObject, useGetAspectObjects } from "external/sources/aspectobject/aspectObject.queries";
import {
  ButtonContainer,
  JsonContent,
  QueryAndMutationExampleContainer,
  ResultsContainer,
  StatusAndResultsContainer,
  StatusContainer,
} from "./QueryAndMutationExample.styled";

export const QueryAndMutationExample = () => {
  const mutation = useCreateAspectObject();
  const query = useGetAspectObjects();

  // Fetching a single aspect object by id and caching it with that id and type
  // const aspectObjectQuery = useGetAspectObject("44EEE7F5A7771594F7349A8A230AB272");

  const exampleAspectObject: AspectObjectLibAm = {
    name: "Test aspect object",
    rdsCode: "A",
    rdsName: "Drilling system",
    purposeName: "Heat (Electrical)",
    parentId: null,
    aspect: Aspect.Function,
    companyId: 0,
    description: "A description goes here",
    symbol: "http://localhost:5001/symbol/018120B75674ABF18AB2F07691D1865C.svg",
    attributes: [],
    aspectObjectTerminals: [
      {
        terminalId: 0,
        minQuantity: 1,
        maxQuantity: 1,
        connectorDirection: 0,
      },
      {
        terminalId: 0,
        minQuantity: 1,
        maxQuantity: 1,
        connectorDirection: 1,
      },
    ],
    selectedAttributePredefined: [],
    typeReferences: [],
    version: "1.0",
  };

  return (
    <QueryAndMutationExampleContainer>
      <h1>Aspect objects example</h1>
      <p>Throttle the network speed in your browser to view state changes more clearly</p>

      <StatusAndResultsContainer>
        <StatusContainer>
          <div>
            <b>Query status: </b>
            <i>
              {query.isLoading && "Loading aspect objects 🔄"}
              {query.isFetching && !query.isLoading && "Fetching aspect objects after changes 🔄"}
              {query.isSuccess && !query.isLoading && !query.isFetching && "Success fetching aspect objects ✅"}
              {query.isError && "Error fetching aspect objects ❌"}
            </i>
          </div>

          <div>
            <b>Mutation status: </b>
            <i>
              {mutation.isLoading && "Creating aspect object 🔄"}
              {mutation.isSuccess && "Success creating aspect object ✅"}
              {mutation.isError && "Error creating aspect object ❌"}
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
        <button onClick={() => mutation.mutate(exampleAspectObject)}>Add valid aspect object ✅</button>
        <button onClick={() => mutation.mutate({ ...exampleAspectObject, parentId: 0 })}>Add invalid aspect object ❌</button>
      </ButtonContainer>
    </QueryAndMutationExampleContainer>
  );
};
