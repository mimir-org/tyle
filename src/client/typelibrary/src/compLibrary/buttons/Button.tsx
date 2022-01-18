import { ButtonContainer } from "./styled";

interface Props {
  onClick: () => void;
  text: string;
  icon?: string;
}

/**
 * Component for a generic button.
 * @param interface
 * @returns a button with text and an optional icon.
 */
const Button = ({ onClick, text }: Props) => (
  <ButtonContainer onClick={() => onClick()}>
    <div className="button-text">{text}</div>
  </ButtonContainer>
);

export default Button;
