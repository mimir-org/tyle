import { ChangeEvent, useEffect, useState } from "react";
import { NumericInput } from ".";

interface Props {
  value: string;
  onChange: (quantity: number) => void;
}

const NumericValueInput = ({ value, onChange }: Props) => {
  const [quantity, setQuantity] = useState(1);

  useEffect(() => {
    if (value) {
      const num = parseInt(value) || 0;
      setQuantity(num);
    }
  }, [value]);

  const handleChange = (e: ChangeEvent<HTMLInputElement>) => {
    const num = parseInt(e.target.value) || 0;
    setQuantity(num);
  };

  return (
    <NumericInput>
      <label className={"quantity"}>
        <input
          type="number"
          min="0"
          max="30"
          placeholder="0"
          onChange={handleChange}
          onBlur={() => onChange(quantity)}
          value={quantity}
        />
        <span className="number"/>
      </label>
    </NumericInput>
  );
};

export default NumericValueInput;
