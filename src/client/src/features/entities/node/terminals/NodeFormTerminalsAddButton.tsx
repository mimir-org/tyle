import { PlusSm } from "@styled-icons/heroicons-outline";
import { Button } from "complib/buttons";
import { useTranslation } from "react-i18next";

export const NodeFormTerminalsAddButton = ({ onClick }: { onClick: () => void }) => {
  const { t } = useTranslation();

  return (
    <Button icon={<PlusSm />} iconOnly onClick={onClick}>
      {t("terminals.add")}
    </Button>
  );
};
