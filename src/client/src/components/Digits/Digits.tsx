import useDigitInput from "react-digit-input";
import { DigitsInput, DigitsInputContainer } from "./Digits.styled";

interface DigitsProps {
  length?: number;
  value?: string;
  onChange: (value: string) => void;
}

/**
 * A component for inputting distinct digits, where each digit is presented by their own visual boundary.
 *
 * @param length how many digits the input should render
 * @param value current value of field
 * @param onChange called when field changes
 * @constructor
 */
const Digits = ({ length = 6, value = "", onChange }: DigitsProps) => {
  const digits = useDigitInput({
    acceptedCharacters: /^[0-9]$/,
    length: length,
    value,
    onChange,
  });

  return (
    <DigitsInputContainer>
      {Array.from({ length: length }, (x, i) => (
        <DigitsInput key={i} inputMode={"decimal"} {...digits[i]} placeholder={(i + 1).toString()} />
      ))}
    </DigitsInputContainer>
  );
};

export default Digits;
