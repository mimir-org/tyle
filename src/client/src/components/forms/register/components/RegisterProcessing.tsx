import { Spinner } from "../../../../compLibrary/spinner";
import { RegisterProcessingContainer, RegisterProcessingText } from "./RegisterProcessing.styled";

export const RegisterProcessing = () => {
  return (
    <RegisterProcessingContainer>
      <RegisterProcessingText>Registering your user</RegisterProcessingText>
      <Spinner />
    </RegisterProcessingContainer>
  );
};
