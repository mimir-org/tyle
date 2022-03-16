import { FullPageSpinnerContainer } from "./FullPageSpinner.styled";
import { Spinner } from "./Spinner";

interface Props {
  text?: string;
}

export const FullPageSpinner = ({ text }: Props) => {
  return (
    <FullPageSpinnerContainer>
      <h1>{text}</h1>
      <Spinner />
    </FullPageSpinnerContainer>
  );
};
