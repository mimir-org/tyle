import { SwitchContainer, Thumb, Wrapper } from "./Switch.styled";

interface SwitchProps {
  checked: boolean;
  onCheckedChange: (checked: boolean) => void;
  children?: React.ReactNode;
}

const Switch = ({ checked, onCheckedChange, children }: SwitchProps) => {
  return (
    <Wrapper>
      {children}
      <SwitchContainer checked={checked} onCheckedChange={onCheckedChange}>
        <Thumb />
      </SwitchContainer>
    </Wrapper>
  );
};

export default Switch;
