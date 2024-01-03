import { ReactElement } from "react";

export interface Actionable {
  actionable: boolean;
  actionIcon: string | ReactElement;
  actionText: string;
  onAction: () => void;
  dangerousAction?: boolean;
}
