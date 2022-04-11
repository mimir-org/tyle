import styled from "styled-components/macro";

export const QueryAndMutationExampleContainer = styled.div`
  display: flex;
  gap: 20px;
  flex-direction: column;
  padding: 10px;
  background-color: white;
  border: 5px solid hsl(230, 20%, 50%);
`;

export const StatusAndResultsContainer = styled.div`
  display: flex;
  flex-wrap: wrap;
  gap: 100px;
`;

export const JsonContent = styled.pre`
  white-space: pre-wrap;
`;

export const StatusContainer = styled.div`
  width: 400px;
  overflow: auto;
`;

export const ResultsContainer = styled.div`
  width: 600px;
  height: 500px;
  overflow: auto;
`;

export const ButtonContainer = styled.div`
  display: flex;
  justify-content: space-around;
`;
