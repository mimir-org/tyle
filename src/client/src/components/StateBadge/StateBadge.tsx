import { State } from "@mimirorg/typelibrary-types";
import Badge from "./Badge";
import { Text } from "@mimirorg/component-library";
import { useTranslation } from "react-i18next";

interface StateBadgeProps {
  state: State | string;
}

function getStateString(state: State | string) {
  return typeof state === "string" ? state : State[state];
}

/**
 * Component which displays a badge with the state of an entity.
 * @param state
 * @constructor
 */

const StateBadge = ({ state }: StateBadgeProps) => {
  const { t } = useTranslation("explore");
  const stateString = getStateString(state);

  switch (stateString) {
    case "Review":
      return (
        <Badge variant={"info"}>
          <Text variant={"body-medium"}>{t(`search.item.states.review`)}</Text>
        </Badge>
      );
    case "Approved":
      return (
        <Badge variant={"success"}>
          <Text variant={"body-medium"}>{t(`search.item.states.approved`)}</Text>
        </Badge>
      );
    case "Draft":
      return (
        <Badge variant={"info"}>
          <Text variant={"body-medium"}>{t(`search.item.states.draft`)}</Text>
        </Badge>
      );
    default:
      return (
        <Badge variant={"info"}>
          <Text variant={"body-medium"}>{stateString}</Text>
        </Badge>
      );
  }
};

export default StateBadge;