import { PlusSmall } from "@styled-icons/heroicons-outline";
import { Button } from "complib/buttons";

interface FormAddButtonProps {
  buttonText: string;
  onClick: () => void;
}

/**
 * Component shows a button with a plus icon
 *
 * @param buttonText text for screen-readers
 * @param onClick action when clicking the button
 * @constructor
 */
export const FormAddButton = ({ buttonText, onClick }: FormAddButtonProps) => (
  <Button icon={<PlusSmall />} iconOnly onClick={onClick}>
    {buttonText}
  </Button>
);
