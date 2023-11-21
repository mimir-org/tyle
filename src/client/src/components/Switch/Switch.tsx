import { Thumb, Wrapper } from "./Switch.styled";

interface SwitchProps {
  checked: boolean;
  onCheckedChange: (checked: boolean) => void;
}

const Switch = ({ checked, onCheckedChange }: SwitchProps) => {
  return (
    <Wrapper checked={checked} onCheckedChange={onCheckedChange}>
      <Thumb />
    </Wrapper>
  );
};

export default Switch;
