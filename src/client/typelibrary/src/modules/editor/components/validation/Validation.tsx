import { ValidationContainer, ValidationMessage } from "./styled";

interface ValidatorContainerProps {
  visible: boolean;
  message: string;
  children: JSX.Element[] | JSX.Element;
  flex?: string | number;
  minWidth?: string;
  maxWidth?: string;
}

const Validation = ({ visible, message, children, flex, minWidth, maxWidth }: ValidatorContainerProps) => (
  <ValidationContainer flex={flex} minWidth={minWidth} maxWidth={maxWidth}>
    {children}
    <ValidationMessage>{visible && message}</ValidationMessage>
  </ValidationContainer>
);

export default Validation;
