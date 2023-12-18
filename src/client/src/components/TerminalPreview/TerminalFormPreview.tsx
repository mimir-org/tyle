import { TerminalFormFields } from "components/TerminalForm/TerminalForm.helpers";
import { Control, useWatch } from "react-hook-form";
import TerminalPreview from "./TerminalPreview";

interface TerminalFormPreviewProps {
  control: Control<TerminalFormFields>;
}

const TerminalFormPreview = ({ control }: TerminalFormPreviewProps) => {
  const name = useWatch({ control, name: "name" });

  return <TerminalPreview variant={"large"} name={name ? name : "Name"} color={"black"} />;
};

export default TerminalFormPreview;
