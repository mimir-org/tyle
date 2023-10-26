// import { Aspect, BlockLibAm } from "@mimirorg/typelibrary-types";
// import { useCreateBlock, useGetBlocks } from "external/sources/block/block.queries";
// import {
//   ButtonContainer,
//   JsonContent,
//   QueryAndMutationExampleContainer,
//   ResultsContainer,
//   StatusAndResultsContainer,
//   StatusContainer,
// } from "./QueryAndMutationExample.styled";

// export const QueryAndMutationExample = () => {
//   const mutation = useCreateBlock();
//   const query = useGetBlocks();

//   // Fetching a single block by id and caching it with that id and type
//   // const blockQuery = useGetBlock("44EEE7F5A7771594F7349A8A230AB272");

//   const exampleBlock: BlockLibAm = {
//     name: "Test block",
//     rdsId: "A",
//     purposeName: "Heat (Electrical)",
//     aspect: Aspect.Function,
//     companyId: 0,
//     description: "A description goes here",
//     symbol: "http://localhost:5001/symbol/018120B75674ABF18AB2F07691D1865C.svg",
//     attributes: [],
//     attributeGroups: [],
//     blockTerminals: [
//       {
//         terminalId: "",
//         minQuantity: 1,
//         maxQuantity: 1,
//         connectorDirection: 0,
//       },
//       {
//         terminalId: "",
//         minQuantity: 1,
//         maxQuantity: 1,
//         connectorDirection: 1,
//       },
//     ],
//     selectedAttributePredefined: [],
//     typeReference: "",
//     version: "1.0",
//   };

//   return (
//     <QueryAndMutationExampleContainer>
//       <h1>Blocks example</h1>
//       <p>Throttle the network speed in your browser to view state changes more clearly</p>

//       <StatusAndResultsContainer>
//         <StatusContainer>
//           <div>
//             <b>Query status: </b>
//             <i>
//               {query.isLoading && "Loading blocks 🔄"}
//               {query.isFetching && !query.isLoading && "Fetching blocks after changes 🔄"}
//               {query.isSuccess && !query.isLoading && !query.isFetching && "Success fetching blocks ✅"}
//               {query.isError && "Error fetching blocks ❌"}
//             </i>
//           </div>

//           <div>
//             <b>Mutation status: </b>
//             <i>
//               {mutation.isLoading && "Creating block 🔄"}
//               {mutation.isSuccess && "Success creating block ✅"}
//               {mutation.isError && "Error creating block ❌"}
//             </i>
//             {mutation.isError && <JsonContent>{JSON.stringify(mutation.error, null, 2)}</JsonContent>}
//           </div>
//         </StatusContainer>

//         <ResultsContainer>
//           {query.data?.map((n) => <JsonContent key={n.id}>{JSON.stringify(n, null, 2)}</JsonContent>)}
//         </ResultsContainer>
//       </StatusAndResultsContainer>

//       <ButtonContainer>
//         <button onClick={() => mutation.mutate(exampleBlock)}>Add valid block ✅</button>
//         <button onClick={() => mutation.mutate({ ...exampleBlock, rdsId: "INVALID_ID" })}>Add invalid block ❌</button>
//       </ButtonContainer>
//     </QueryAndMutationExampleContainer>
//   );
// };
