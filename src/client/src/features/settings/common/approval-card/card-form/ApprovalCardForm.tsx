import { Button, Flexbox, Text } from "@mimirorg/component-library";
import { useTranslation } from "react-i18next";
import { useTheme } from "styled-components";
import { AttributeView } from "common/types/attributes/attributeView";
import { usePatchAttributeState } from "external/sources/attribute/attribute.queries";
import { State } from "common/types/common/state";
import { useSubmissionToast } from "features/entities/common/utils/useSubmissionToast";

export interface ApprovalCardFormProps {
  item: AttributeView;
}

export const ApprovalCardForm = ({
  item,
}: ApprovalCardFormProps) => {
  const { t } = useTranslation(["settings"]);
  const theme = useTheme();

  const patchMutationAttribute = usePatchAttributeState(item.id);

  const toast = useSubmissionToast(t("attribute.title"));

  return (
    <Flexbox flexFlow={"row"} justifyContent={"space-between"} style={{ marginTop: "8px" }}>
        <Text variant={"body-large"}>{`Requesting to be approved.`}</Text>
        <Flexbox justifyContent={"center"} alignItems={"center"} flexFlow="row" gap={theme.mimirorg.spacing.base}>
          <Button dangerousAction type={"button"} onClick={() => 
            toast(patchMutationAttribute.mutateAsync({ state: State.Draft }))
          }>
                  {t("common.approval.reject")}
          </Button>
          <Button type={"button"} onClick={() =>
            toast(patchMutationAttribute.mutateAsync({ state: State.Approved }))
          }>{t("common.approval.submit")}</Button>
        </Flexbox>
      </Flexbox>
  );
};
