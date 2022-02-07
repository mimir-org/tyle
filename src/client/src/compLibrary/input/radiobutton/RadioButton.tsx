import { RadioButtonWrapper } from "./styled";

interface Props {
  isChecked: boolean;
  onChange: (e) => void;
  id: string;
}

/**
 * Component for a radio button in Mimir.
 * @param interface
 * @returns a radio button.
 */
const RadioButton = ({ isChecked, onChange, id }: Props) => (
  <RadioButtonWrapper>
    <input type="radio" value={id} key={id} checked={isChecked ?? false} id={id} onChange={(e) => onChange(e)} />
    <div className="checkmark-circle" />
  </RadioButtonWrapper>
);

export default RadioButton;
