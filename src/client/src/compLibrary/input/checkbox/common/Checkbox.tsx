import { CheckboxWrapper } from "./styled";
import { Color } from "../../../colors";
import { CheckmarkCheckedComponent, CheckmarkEmptyComponent } from "../../../../assets/icons/checkmark";

interface Props {
  isChecked: boolean;
  onChange: () => void;
  color?: string;
  readOnly?: boolean | false;
  id?: string;
  marginLeft?: number;
}

/**
 * A generic checkbox for Mimir.
 * @param interface
 * @returns a checkbox.
 */
const Checkbox = ({ isChecked, onChange, color = Color.GreyHeader, readOnly, id, marginLeft = 0 }: Props) => (
  <CheckboxWrapper color={color} marginLeft={marginLeft}>
    {isChecked ? <CheckmarkCheckedComponent /> : <CheckmarkEmptyComponent />}
    <input id={id} type="checkbox" readOnly={readOnly} key={id} checked={isChecked} onChange={() => onChange()} />
  </CheckboxWrapper>
);

export default Checkbox;
