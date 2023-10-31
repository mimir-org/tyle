import { Button, Flexbox, Text } from "@mimirorg/component-library";
import { useTranslation } from "react-i18next";
import { useTheme } from "styled-components";
import { AttributeView } from "common/types/attributes/attributeView";
import { State } from "common/types/common/state";
import { useSubmissionToast } from "helpers/form.helpers";
import { TerminalView } from "common/types/terminals/terminalView";
import { usePatchStateMutation } from "./ApprovalCardForm.helpers";
import { BlockView } from "common/types/blocks/blockView";

export interface ApprovalCardFormProps {
  item: AttributeView | TerminalView | BlockView;
  itemType: "attribute" | "terminal" | "block";
}

export const ApprovalCardForm = ({ item, itemType }: ApprovalCardFormProps) => {
  const { t } = useTranslation(["settings"]);
  const theme = useTheme();

  const patchStateMutation = usePatchStateMutation(item, itemType);

  const toast = useSubmissionToast(t("attribute.title"));

  return (
    <Flexbox flexFlow={"row"} justifyContent={"space-between"} style={{ marginTop: "8px" }}>
      <Text variant={"body-large"}>{`Requesting to be approved.`}</Text>
      <Flexbox justifyContent={"center"} alignItems={"center"} flexFlow="row" gap={theme.mimirorg.spacing.base}>
        <Button
          dangerousAction
          type={"button"}
          onClick={() => toast(patchStateMutation.mutateAsync({ state: State.Draft }))}
        >
          {t("common.approval.reject")}
        </Button>
        <Button type={"button"} onClick={() => toast(patchStateMutation.mutateAsync({ state: State.Approved }))}>
          {t("common.approval.submit")}
        </Button>
      </Flexbox>
    </Flexbox>
  );
};
