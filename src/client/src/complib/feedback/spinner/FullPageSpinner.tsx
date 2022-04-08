import { FullPageSpinnerContainer } from "./FullPageSpinner.styled";
import { Spinner } from "./Spinner";
import { Heading } from "../../text";

interface Props {
  text?: string;
}

/**
 * Spinner that is centered and fills the whole screen
 * @param text Text which is shown to the left of the spinner animation
 * @constructor
 */
export const FullPageSpinner = ({ text }: Props) => (
  <FullPageSpinnerContainer>
    <Heading>{text}</Heading>
    <Spinner />
  </FullPageSpinnerContainer>
);
