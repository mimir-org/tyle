import { TerminalPreview } from "common/components/terminal/TerminalPreview";
import { Control, useWatch } from "react-hook-form";
import { useTranslation } from "react-i18next";
import { FormTerminalLib } from "./types/formTerminalLib";

interface TerminalFormPreviewProps {
  control: Control<FormTerminalLib>;
}

export const TerminalFormPreview = ({ control }: TerminalFormPreviewProps) => {
  const { t } = useTranslation();
  const name = useWatch({ control, name: "name" });
  const color = useWatch({ control, name: "color" });

  return <TerminalPreview variant={"large"} name={name ? name : t("terminal.name")} color={color} />;
};
