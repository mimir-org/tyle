import { PlusSmall } from "@styled-icons/heroicons-outline";
import { Button } from "@mimirorg/component-library";
import { useTranslation } from "react-i18next";

export const BlockFormTerminalsAddButton = ({ onClick }: { onClick: () => void }) => {
  const { t } = useTranslation("entities");

  return (
    <Button icon={<PlusSmall />} iconOnly onClick={onClick}>
      {t("block.terminals.add")}
    </Button>
  );
};
