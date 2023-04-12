import { PlusSmall } from "@styled-icons/heroicons-outline";
import { Button } from "complib/buttons";
import { useTranslation } from "react-i18next";

export const AspectObjectFormTerminalsAddButton = ({ onClick }: { onClick: () => void }) => {
  const { t } = useTranslation("entities");

  return (
    <Button icon={<PlusSmall />} iconOnly onClick={onClick}>
      {t("aspectObject.terminals.add")}
    </Button>
  );
};
