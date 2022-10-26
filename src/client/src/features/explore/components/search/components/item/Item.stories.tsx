import { ComponentMeta, ComponentStory } from "@storybook/react";
import { Button } from "../../../../../../complib/buttons";
import { NodeProps } from "../../../../../../content/common/node/Node";
import { Default as Node } from "../../../../../../content/common/node/Node.stories";
import { Default as ItemDescription } from "./ItemDescription.stories";
import { Item } from "./Item";
import { ItemDescriptionProps } from "./ItemDescription";

export default {
  title: "Explore/Search/Item/Item",
  component: Item,
} as ComponentMeta<typeof Item>;

const Template: ComponentStory<typeof Item> = (args) => <Item {...args} />;

export const Default = Template.bind({});
Default.args = {
  isSelected: false,
  preview: <Node {...(Node.args as NodeProps)} />,
  description: <ItemDescription {...(ItemDescription.args as ItemDescriptionProps)} />,
  actions: (
    <>
      <Button>Action A</Button>
      <Button>Action B</Button>
      <Button>Action C</Button>
    </>
  ),
};

export const Selected = Template.bind({});
Selected.args = {
  ...Default.args,
  isSelected: true,
};
