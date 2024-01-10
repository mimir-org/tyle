import Heading from "components/Heading";
import Spinner from "components/Spinner";
import { useTheme } from "styled-components";
import { FullPageSpinnerContainer } from "./FullPageSpinner.styled";

interface Props {
  text?: string;
}

/**
 * Spinner that is centered and fills the whole screen
 * @param text Text which is shown to the left of the spinner animation
 * @constructor
 */
const FullPageSpinner = ({ text }: Props) => {
  const theme = useTheme();

  return (
    <FullPageSpinnerContainer>
      <Heading color={theme.tyle.color.background.on}>{text}</Heading>
      <Spinner disabled={false} />
    </FullPageSpinnerContainer>
  );
};

export default FullPageSpinner;
