import { ButtonContainer } from "./styled";

interface Props {
  onClick: () => void;
  text: string;
  icon?: string;
  disabled?: boolean;
}

/**
 * Component for a generic button in Mimir.
 * @param interface
 * @returns a button with text and an optional icon.
 */
const Button = ({ onClick, text, icon = null, disabled = false }: Props) => (
  <ButtonContainer
    onClick={() => onClick()}
    icon={icon !== null}
    disabled={disabled}
  >
    <div className="button-text">{text}</div>
    {icon && <img src={icon} alt="icon" />}
  </ButtonContainer>
);

export default Button;
