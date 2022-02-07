import { ExitButtonContainer } from "./ExitButton.styled";
import { VisuallyHidden } from "../../util";
import { Icon } from "../../icon";
import { CloseIcon } from "../../../assets/icons/close";

interface Props {
  onClick: () => void;
}

const ExitButton = ({ onClick }: Props) => (
  <ExitButtonContainer onClick={() => onClick()}>
    <VisuallyHidden>Close window</VisuallyHidden>
    <Icon size={10} src={CloseIcon} alt="" />
  </ExitButtonContainer>
);

export default ExitButton;
