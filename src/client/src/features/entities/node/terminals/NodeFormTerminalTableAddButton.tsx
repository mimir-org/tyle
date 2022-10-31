import { PlusSm } from "@styled-icons/heroicons-outline";
import { Button } from "complib/buttons";
import { useTranslation } from "react-i18next";

export const NodeFormTerminalTableAddButton = ({ onClick }: { onClick: () => void }) => {
  const { t } = useTranslation("translation", { keyPrefix: "terminals" });

  return (
    <Button icon={<PlusSm />} iconOnly onClick={onClick}>
      {t("add")}
    </Button>
  );
};
