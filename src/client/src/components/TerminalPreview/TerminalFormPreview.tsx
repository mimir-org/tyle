import { TerminalFormFields } from "components/TerminalForm/TerminalForm.helpers";
import { Control, useWatch } from "react-hook-form";
import { useTranslation } from "react-i18next";
import TerminalPreview from "./TerminalPreview";

interface TerminalFormPreviewProps {
  control: Control<TerminalFormFields>;
}

const TerminalFormPreview = ({ control }: TerminalFormPreviewProps) => {
  const { t } = useTranslation("entities");
  const name = useWatch({ control, name: "name" });

  return <TerminalPreview variant={"large"} name={name ? name : t("terminal.name")} color={"black"} />;
};

export default TerminalFormPreview;
