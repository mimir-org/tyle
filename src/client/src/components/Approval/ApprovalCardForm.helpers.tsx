import { Flexbox, Text, toast } from "@mimirorg/component-library";
import { usePatchAttributeState } from "api/attribute.queries";
import { usePatchBlockState } from "api/block.queries";
import { usePatchTerminalState } from "api/terminal.queries";
import { AxiosError } from "axios";
import { AttributeView } from "common/types/attributes/attributeView";
import { BlockView } from "common/types/blocks/blockView";
import { TerminalView } from "common/types/terminals/terminalView";
import { FormApproval, mapFormApprovalToApiModel } from "components/Approval/formApproval";
import { useTranslation } from "react-i18next";
import { useTheme } from "styled-components";

export const usePatchStateMutation = (
  item: AttributeView | TerminalView | BlockView,
  itemType: "attribute" | "terminal" | "block",
) => {
  const patchAttributeStateMutation = usePatchAttributeState(item.id);
  const patchTerminalStateMutation = usePatchTerminalState(item.id);
  const patchBlockStateMutation = usePatchBlockState(item.id);

  switch (itemType) {
    case "attribute":
      return patchAttributeStateMutation;
    case "terminal":
      return patchTerminalStateMutation;
    case "block":
      return patchBlockStateMutation;
  }
};

/**
 * Shows a toast while an approval is sent to server.
 * Shows an undo action on the toast after the approval is sent.
 */
export const useApprovalToasts = (id: string) => {
  const theme = useTheme();
  const { t } = useTranslation("settings");
  // const patchMutationBlock = usePatchBlockState();
  //const patchMutationTerminal = usePatchTerminalState();
  const patchMutationAttribute = usePatchAttributeState(id);

  let mutationPromise = {} as Promise<AttributeView>;

  return async (id: string, submission: FormApproval) => {
    switch (submission.objectType) {
      case "Block":
        //  mutationPromise = patchMutationBlock.mutateAsync(mapFormApprovalToApiModel(submission));
        break;
      case "Terminal":
        //mutationPromise = patchMutationTerminal.mutateAsync(mapFormApprovalToApiModel(submission));
        break;
      case "Attribute":
        mutationPromise = patchMutationAttribute.mutateAsync(mapFormApprovalToApiModel(submission));
        break;
      default:
        throw new Error("Unable to approve, reject or undo, ask a developer for help resolving this issue.");
    }

    return toast.promise(
      mutationPromise,
      {
        loading: t("common.approval.processing.loading"),
        success: (
          <Flexbox alignContent="center" alignItems="center">
            <Text
              variant={"label-large"}
              spacing={{ mr: theme.mimirorg.spacing.base }}
              color={theme.mimirorg.color.pure.base}
            >
              {t("common.approval.processing.success")}
            </Text>
          </Flexbox>
        ),
        error: (error: AxiosError) => {
          if (error.response?.status === 403)
            return t("common.approval.processing.error.403", { data: error.response?.data ?? "" });
          return t("common.approval.processing.error.default");
        },
      },
      {
        success: {
          style: {
            backgroundColor: theme.mimirorg.color.tertiary.base,
          },
        },
      },
    );
  };
};
