import uuid
from typing import Dict, Union

import pydantic
from pydantic import Field

from src.models.solution.pydantic_config import ConfigPydanticDataclass


@pydantic.dataclasses.dataclass(config=ConfigPydanticDataclass)
class ScheduleSolutionMetadata:
    number_of_closings_for_each_emp: Dict[Union[uuid.UUID, str], int]
    number_of_mornings_for_each_emp:  Dict[Union[uuid.UUID, str], int]
    number_of_shift_for_each_emp: Dict[Union[uuid.UUID, str], int]

    # shift id, employee id
    schedule: Dict[str, str]
