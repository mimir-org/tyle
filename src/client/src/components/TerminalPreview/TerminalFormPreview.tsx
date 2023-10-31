import { TerminalPreview } from "components/TerminalPreview/TerminalPreview";
import { TerminalFormFields } from "components/TerminalForm/TerminalForm.helpers";
import { Control, useWatch } from "react-hook-form";
import { useTranslation } from "react-i18next";

interface TerminalFormPreviewProps {
  control: Control<TerminalFormFields>;
}

export const TerminalFormPreview = ({ control }: TerminalFormPreviewProps) => {
  const { t } = useTranslation("entities");
  const name = useWatch({ control, name: "name" });

  return <TerminalPreview variant={"large"} name={name ? name : t("terminal.name")} color={"black"} />;
};
