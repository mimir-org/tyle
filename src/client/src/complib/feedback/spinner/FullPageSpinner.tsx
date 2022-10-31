import { FullPageSpinnerContainer } from "complib/feedback/spinner/FullPageSpinner.styled";
import { Spinner } from "complib/feedback/spinner/Spinner";
import { Heading } from "complib/text";
import { useTheme } from "styled-components";

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
