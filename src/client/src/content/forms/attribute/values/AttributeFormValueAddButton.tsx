import { PlusSm } from "@styled-icons/heroicons-outline";
import { useTranslation } from "react-i18next";
import { Button } from "../../../../complib/buttons";

export const AttributeFormValueAddButton = ({ onClick }: { onClick: () => void }) => {
  const { t } = useTranslation("translation", { keyPrefix: "attribute.values" });

  return (
    <Button icon={<PlusSm />} iconOnly onClick={onClick}>
      {t("add")}
    </Button>
  );
};
