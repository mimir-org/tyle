import { Text } from "@mimirorg/component-library";
import { State } from "types/common/state";
import Badge from "./Badge";

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
  const stateString = getStateString(state);

  switch (stateString) {
    case "Review":
      return (
        <Badge variant={"info"}>
          <Text variant={"body-medium"}>Pending approval</Text>
        </Badge>
      );
    case "Approved":
      return (
        <Badge variant={"success"}>
          <Text variant={"body-medium"}>Approved</Text>
        </Badge>
      );
    case "Draft":
      return (
        <Badge variant={"info"}>
          <Text variant={"body-medium"}>Draft</Text>
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
