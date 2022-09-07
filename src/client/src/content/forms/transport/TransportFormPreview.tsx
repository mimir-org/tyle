import { Control, useWatch } from "react-hook-form";
import { useTranslation } from "react-i18next";
import { TransportPreview } from "../../common/transport";
import { FormTransportLib } from "./types/formTransportLib";

interface TransportFormPreviewProps {
  control: Control<FormTransportLib>;
}

export const TransportFormPreview = ({ control }: TransportFormPreviewProps) => {
  const { t } = useTranslation();
  const name = useWatch({ control, name: "name" });
  const terminalColor = useWatch({ control, name: "terminalColor" });

  return <TransportPreview variant={"large"} name={name ? name : t("terminal.name")} color={terminalColor} />;
};
