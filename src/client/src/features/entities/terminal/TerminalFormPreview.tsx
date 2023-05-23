import { TerminalPreview } from "features/common/terminal/TerminalPreview";
import { FormTerminalLib } from "features/entities/terminal/types/formTerminalLib";
import { Control, useWatch } from "react-hook-form";
import { useTranslation } from "react-i18next";

interface TerminalFormPreviewProps {
  control: Control<FormTerminalLib>;
}

export const TerminalFormPreview = ({ control }: TerminalFormPreviewProps) => {
  const { t } = useTranslation("entities");
  const name = useWatch({ control, name: "name" });
  const color = useWatch({ control, name: "color" });

  return <TerminalPreview variant={"large"} name={name ? name : t("terminal.name")} color={color} />;
};
