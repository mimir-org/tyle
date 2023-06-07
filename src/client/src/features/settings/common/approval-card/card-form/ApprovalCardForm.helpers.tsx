import { ApprovalDataCm, State } from "@mimirorg/typelibrary-types";
import { toast } from "complib/data-display";
import { Text } from "complib/text";
import { useTranslation } from "react-i18next";
import { useTheme } from "styled-components";
import {
  FormApproval,
  mapFormApprovalToApiModel,
} from "features/settings/common/approval-card/card-form/types/formApproval";
import { Flexbox } from "complib/layouts/Flexbox";
import { usePatchTerminalState } from "external/sources/terminal/terminal.queries";
import { usePatchAspectObjectState } from "external/sources/aspectobject/aspectObject.queries";
import { usePatchUnitState } from "../../../../../external/sources/unit/unit.queries";
import { usePatchQuantityDatumState } from "../../../../../external/sources/datum/quantityDatum.queries";
import { usePatchRdsState } from "../../../../../external/sources/rds/rds.queries";
import { usePatchAttributeState } from "../../../../../external/sources/attribute/attribute.queries";
import { AxiosError } from "axios";

/**
 * Shows a toast while an approval is sent to server.
 * Shows an undo action on the toast after the approval is sent.
 */
export const useApprovalToasts = () => {
  const theme = useTheme();
  const { t } = useTranslation("settings");
  const patchMutationAspectObject = usePatchAspectObjectState();
  const patchMutationTerminal = usePatchTerminalState();
  const patchMutationUnit = usePatchUnitState();
  const patchMutationQuantityDatum = usePatchQuantityDatumState();
  const patchMutationRds = usePatchRdsState();
  const patchMutationAttribute = usePatchAttributeState();

  let mutationPromise = {} as Promise<ApprovalDataCm>;

  return async (id: string, submission: FormApproval) => {
    switch (submission.objectType) {
      case "AspectObject":
        mutationPromise = patchMutationAspectObject.mutateAsync(mapFormApprovalToApiModel(submission));
        break;
      case "Terminal":
        mutationPromise = patchMutationTerminal.mutateAsync(mapFormApprovalToApiModel(submission));
        break;
      case "Unit":
        mutationPromise = patchMutationUnit.mutateAsync(mapFormApprovalToApiModel(submission));
        break;
      case "Quantity datum":
        mutationPromise = patchMutationQuantityDatum.mutateAsync(mapFormApprovalToApiModel(submission));
        break;
      case "Rds":
        mutationPromise = patchMutationRds.mutateAsync(mapFormApprovalToApiModel(submission));
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
            <Text variant={"label-large"} mr={theme.tyle.spacing.base} color={theme.tyle.color.sys.pure.base}>
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
            backgroundColor: theme.tyle.color.sys.tertiary.base,
          },
        },
      }
    );
  };
};

/**
 * Find the next state from current state.
 * If current state is not a valid state, return current state.
 * @param state current state
 * @returns next state
 * @example
 * findNextState(State.Approve) // State.Approved
 * findNextState(State.Delete) // State.Deleted
 * findNextState(State.Approved) // State.Approved
 * findNextState(State.Deleted) // State.Deleted
 */
export const findNextState = (state: State): State => {
  switch (state) {
    case State.Approve:
      return State.Approved;
    case State.Delete:
      return State.Deleted;
    default:
      return state;
  }
};
