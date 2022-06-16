import { useTheme } from "styled-components";
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
export const FullPageSpinner = ({ text }: Props) => {
  const theme = useTheme();

  return (
    <FullPageSpinnerContainer>
      <Heading color={theme.tyle.color.sys.background.on}>{text}</Heading>
      <Spinner />
    </FullPageSpinnerContainer>
  );
};
