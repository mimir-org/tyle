import { PlusSmall } from "@styled-icons/heroicons-outline";
import { Button } from "complib/buttons";
import { useTranslation } from "react-i18next";

export const NodeFormTerminalsAddButton = ({ onClick }: { onClick: () => void }) => {
  const { t } = useTranslation("entities");

  return (
    <Button icon={<PlusSmall />} iconOnly onClick={onClick}>
      {t("node.terminals.add")}
    </Button>
  );
};
