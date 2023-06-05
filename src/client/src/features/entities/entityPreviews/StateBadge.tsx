import { State } from "@mimirorg/typelibrary-types";
import Badge from "../../ui/badges/Badge";
import { Text } from "../../../complib/text";

interface StateBadgeProps {
  state: State;
}

export const StateBadge = ({ state }: StateBadgeProps) => {
  const stateString = State[state];

  switch (state) {
    case State.Delete:
      return (
        <Badge variant={"warning"}>
          <Text variant={"body-medium"}>{stateString}</Text>
        </Badge>
      );
    case State.Deleted:
      return (
        <Badge variant={"error"}>
          <Text variant={"body-medium"}>{stateString}</Text>
        </Badge>
      );
    case State.Approved:
      return (
        <Badge variant={"success"}>
          <Text variant={"body-medium"}>{stateString}</Text>
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
