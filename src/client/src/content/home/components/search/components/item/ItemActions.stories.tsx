import { ComponentMeta } from "@storybook/react";
import { ItemActions } from "./ItemActions";

export default {
  title: "Content/Home/Search/Item/ItemActions",
  component: ItemActions,
} as ComponentMeta<typeof ItemActions>;

export const Default = () => <ItemActions />;
