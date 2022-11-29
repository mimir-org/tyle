import { ComponentMeta, ComponentStory } from "@storybook/react";
import { Button } from "complib/buttons";
import { NodeProps } from "features/common/node/Node";
import { Default as Node } from "features/common/node/Node.stories";
import { Item } from "features/explore/search/components/item/Item";
import { ItemDescriptionProps } from "features/explore/search/components/item/ItemDescription";
import { Default as ItemDescription } from "features/explore/search/components/item/ItemDescription.stories";

export default {
  title: "Features/Explore/Search/Item/Item",
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
